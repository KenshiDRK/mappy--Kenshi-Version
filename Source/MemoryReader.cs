using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

public class MemoryReader {
   //WINAPI for reading memory
   private enum ProcessAccess : uint {
      PROCESS_VM_OPERATION = 0x0008,
      PROCESS_VM_READ = 0x0010,
      PROCESS_VM_WRITE = 0x0020,
      PROCESS_QUERY_INFORMATION = 0x0400,
      PROCESS_QUERY_LIMITED_INFORMATION = 0x1000
   }

   [DllImport("kernel32.dll")]
   private static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
    [DllImport("kernel32.dll")]
   private static extern Int32 CloseHandle(IntPtr hObject);
   [DllImport("kernel32.dll")]
   private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out int lpNumberOfBytesRead);
   [DllImport("kernel32.dll", SetLastError = true)]
   [return: MarshalAs(UnmanagedType.Bool)]
   private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

   //WINAPI for module inspection
   private const int MAX_MODULE_NAME32 = 255;
   private const int MAX_PATH = 260;

   [Flags]
   private enum SnapshotFlags : uint {
      HeapList = 0x00000001,
      Process  = 0x00000002,
      Thread   = 0x00000004,
      Module   = 0x00000008,
      Module32 = 0x00000010,
      Inherit  = 0x80000000,
      All      = 0x0000001F
   };

   [StructLayout(LayoutKind.Sequential)]
   private struct MODULEENTRY32 {
      public UInt32 dwSize;
      public UInt32 th32ModuleID;
      public UInt32 th32ProcessID;
      public UInt32 GlblcntUsage;
      public UInt32 ProccntUsage;
      public IntPtr modBaseAddr;
      public UInt32 modBaseSize;
      public IntPtr hModule;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_MODULE_NAME32 + 1)]
      public string szModule;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
      public string szExePath;
   };

   [DllImport("kernel32.dll", SetLastError=true)]
   private static extern IntPtr CreateToolhelp32Snapshot(SnapshotFlags dwFlags, uint th32ProcessID);
   [DllImport("kernel32.dll")]
   private static extern bool Module32First(IntPtr hSnapshot, ref MODULEENTRY32 lpme);
   [DllImport("kernel32.dll")]
   private static extern bool Module32Next(IntPtr hSnapshot, ref MODULEENTRY32 lpme);

   //private Storage
   private UInt32 m_pid;
   private IntPtr m_hpid;
   private IntPtr m_hbase;

   //-----------------------------------------------------------------------------
   // Construction/Destruction
   //-----------------------------------------------------------------------------
   /// <summary>
   /// Opens the given process for reading
   /// </summary>
   /// <param name="Process">Process object</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(Process Process) {
      return new MemoryReader((uint)Process.Id, Process.MainModule.BaseAddress);
   }
   /// <summary>
   /// Opens the given process for reading and sets the base address for offset reading
   /// </summary>
   /// <param name="Process">Process object</param>
   /// <param name="BaseAddress">Address to calculate pointer of when offset reading</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(Process Process, IntPtr BaseAddress) {
      return new MemoryReader((uint)Process.Id, BaseAddress);
   }
   /// <summary>
   /// Opens the given process for reading and sets the base address to the given module's base address
   /// </summary>
   /// <param name="Process">Process object</param>
   /// <param name="BaseModule">Module name to search for within the target process</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(Process Process, string BaseModule) {
      IntPtr pBase = GetModuleBase((uint)Process.Id, BaseModule);
      if (pBase == IntPtr.Zero)
         return null;
      return new MemoryReader((uint)Process.Id, pBase);
   }
   /// <summary>
   /// Opens the given process for reading
   /// </summary>
   /// <param name="ProcessID">Process ID</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(UInt32 ProcessID) {
      return(new MemoryReader(ProcessID, IntPtr.Zero));
   }
   /// <summary>
   /// Opens the given process for reading and sets the base address for offset reading
   /// </summary>
   /// <param name="ProcessID">Process ID</param>
   /// <param name="BaseAddress">Address to calculate pointer of when offset reading</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(UInt32 ProcessID, IntPtr BaseAddress) {
      return(new MemoryReader(ProcessID, BaseAddress));
   }
   /// <summary>
   /// Opens the given process for reading and sets the base address to the given module's base address
   /// </summary>
   /// <param name="ProcessID">Process ID</param>
   /// <param name="BaseModule">Module name to search for within the target process</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(UInt32 ProcessID, string BaseModule) {
      IntPtr pBase = GetModuleBase(ProcessID, BaseModule);
      if (pBase == IntPtr.Zero)
         return null;
      return(new MemoryReader(ProcessID, pBase));
   }
   /// <summary>
   /// Searches for the given process by name and opens the first one found, if any
   /// </summary>
   /// <param name="ProcessName">Name to perform search against</param>
   /// <returns>MemoryReader with the opened process</returns>
   public static MemoryReader Open(string[] ProcessName) {
      foreach (string Process in ProcessName) 
      {
         System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcessesByName(Process);
        //System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcessesByName(ProcessName);
        if (processlist.Length > 0) {
            return(new MemoryReader((uint)processlist[0].Id, processlist[0].MainModule.BaseAddress));
        }
      }
      return null;
   }
   
   private MemoryReader(UInt32 ProcessID, IntPtr BaseAddress) {
      m_pid = ProcessID;
      m_hbase = BaseAddress;
      m_hpid = OpenProcess(ProcessAccess.PROCESS_VM_READ | ProcessAccess.PROCESS_QUERY_INFORMATION, 1, ProcessID);
      #if VERBOSE
         Debug.WriteLine("MemoryReader: Opened PID " + ProcessID + " with a base address of " + BaseAddress.ToString("X8"));
      #endif
   }
   ~MemoryReader() {
      if (m_hpid != IntPtr.Zero)
         CloseHandle(m_hpid);
   }

   //-----------------------------------------------------------------------------
   // Private Helpers
   //-----------------------------------------------------------------------------
   /// <summary>
   /// Performs a lookup for the baseaddress of the specified module or the given process
   /// <remarks>
   /// Wow is the managed module listing slow! So since this app is already riddled
   /// with pinvoke, what the hell lets use the faster way!
   /// </remarks>
   /// </summary>
   /// <param name="PID">ID of the target process</param>
   /// <param name="ModuleName">Module name to search for</param>
   /// <returns>Pointer to the modules base address</returns>
   private static IntPtr GetModuleBase(uint PID, string ModuleName) {
      //Take a snapshot of the module list
      IntPtr hModuleList = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, PID);
      if (hModuleList == IntPtr.Zero)
         return IntPtr.Zero;

      try {
         MODULEENTRY32 me32 = new MODULEENTRY32();
         me32.dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32));

         //Set cursor at the start and grab the info
         if (!Module32First(hModuleList, ref me32))
            return IntPtr.Zero;

         //If the module matches our target, use its baseaddress. Otherwise, grab the next module in the list
         do {
            if(string.Compare(me32.szModule, ModuleName, true) == 0)
               return me32.modBaseAddr;
         } while(Module32Next(hModuleList, ref me32));
      } finally {
         //gets fired even if return is used
         CloseHandle(hModuleList);
      }
      return IntPtr.Zero;
   }

   private static bool GetModuleInfo(uint PID, string ModuleName, out MODULEENTRY32 ModuleInfo) {
      ModuleInfo = default(MODULEENTRY32);

      //Take a snapshot of the module list
      IntPtr hModuleList = CreateToolhelp32Snapshot(SnapshotFlags.Module | SnapshotFlags.Module32, PID);
      if(hModuleList == IntPtr.Zero)
         return false;

      try {
         MODULEENTRY32 me32 = new MODULEENTRY32();
         me32.dwSize = (uint)Marshal.SizeOf(typeof(MODULEENTRY32));

         //Set cursor at the start and grab the info
         if(!Module32First(hModuleList, ref me32))
            return false;

         //If the module matches our target, use its baseaddress. Otherwise, grab the next module in the list
         do {
            if(string.Compare(me32.szModule, ModuleName, true) == 0) {
               ModuleInfo = me32;
               return true;
            }
         } while(Module32Next(hModuleList, ref me32));
      } finally {
         //gets fired even if return is used
         CloseHandle(hModuleList);
      }
      return false;
   }


   //-----------------------------------------------------------------------------
   // Public Methods
   //-----------------------------------------------------------------------------
   /// <summary>Gets or sets the BaseAddress used to calculate memory offsets</summary>
   public IntPtr BaseAddress {
      get { return m_hbase; }
      set { m_hbase = value; }
   }

   /// <summary>Gets whether the targer process is still running or not</summary>
   public bool HasExited {
      get {
         if(m_hpid == IntPtr.Zero)
            return true;

         uint code;
         if(GetExitCodeProcess(m_hpid, out code))
            return (code != 259);
         return true;
      }
   }

   /// <summary>Close access to the process and release the handle</summary>
   public void Close() {
      if(m_hpid == IntPtr.Zero)
         return;
      CloseHandle(m_hpid);
      m_hpid = IntPtr.Zero;
   }

   /// <summary>
   /// Reads data from the target process at the specified address
   /// </summary>
   /// <param name="Address">Pointer to the location to read</param>
   /// <param name="Length">Number of bytes to fill the buffer with</param>
   /// <param name="data">[out] A buffer to store the data into</param>
   /// <returns>Returns the number of bytes read into the buffer</returns>
   public int ReadMemory(IntPtr Address, int Length, out byte[] data) {
      if (m_hpid == IntPtr.Zero)
         throw new MemoryReaderException("Unable to read the process becuase it was never opened.");
      if (HasExited)
         throw new MemoryReaderException("Unable to read the process becuase it has been closed.");

      byte[] buffer = new byte[Length];
      int iReadCount;

      #if VERBOSE
         Debug.WriteLine("ReadMemory: Attempting to read at address " + Address.ToString("X8"));
      #endif
      #if DEBUG
         if (!ReadProcessMemory(m_hpid, Address, buffer, Length, out iReadCount))
            Debug.WriteLine("ReadMemory: Unable to retrieve bytes from the process. O/S reported error #" + Marshal.GetLastWin32Error().ToString());
      #else
         ReadProcessMemory(m_hpid, Address, buffer, Length, out iReadCount);
      #endif
      data = buffer;
      return iReadCount;
   }
   /// <summary>
   /// Reads data from the target process at the specifiec offset from the previously established base.
   /// <remarks>
   /// If no base has been specified then this will act as if reading a hard address.
   /// </remarks>
   /// </summary>
   /// <param name="Offset">Offset from the base address to read</param>
   /// <param name="Length">Number of bytes to fill the buffer with</param>
   /// <param name="data">[out] A buffer to store the data into</param>
   /// <returns>Returns the number of bytes read into the buffer</returns>
   public int ReadOffset(IntPtr Offset, int Length, out byte[] data) {
      int baseoffset = (int)this.m_hbase + (int)Offset;
      return ReadMemory((IntPtr)baseoffset, Length, out data);
   }
   /// <summary>
   /// Convert the binary data at the address into the specified data type or structure.
   /// </summary>
   /// <typeparam name="T">Type to coerce the data into</typeparam>
   /// <param name="Address">Address to begin reading</param>
   /// <returns>Returns the type filled with bytes from the requested address or a default type if the function fails.</returns>
   public T ReadStruct<T>(IntPtr Address) {
      byte[] buffer;
      int cnt = ReadMemory(Address, Marshal.SizeOf(typeof(T)), out buffer);
      if(cnt > 0) {
         GCHandle pinned = GCHandle.Alloc(buffer, GCHandleType.Pinned);
         try {
            return (T)Marshal.PtrToStructure(pinned.AddrOfPinnedObject(), typeof(T));
#if DEBUG
         } catch(Exception ex) {
            Debug.WriteLine("ReadStruct: Unable to coerce foreign data into <" + typeof(T).ToString() + ">: " + ex.Message);
#else
         } catch {
#endif
         } finally {
            pinned.Free();
         }
      }
      return default(T);
    }

   /// <summary>
   /// Convert the binary data at the address into an array of structures of the specified type and length.
   /// </summary>
   /// <typeparam name="T">Type to coerce the data into</typeparam>
   /// <param name="Address">Address to begin reading</param>
   /// <param name="Count">The number of structures to read</param>
   /// <returns>Returns a filled array of the specified type or a default type if the function fails.</returns>
   public T[] ReadStructArray<T>(IntPtr Address, int Count) {
      if(Count <= 0)
         return default(T[]);

      //Read in the number of bytes required for each array index
      byte[] buffer;
      int cnt = ReadMemory(Address, Marshal.SizeOf(typeof(T)) * Count, out buffer);
      if(cnt > 0) {
         GCHandle pinned = GCHandle.Alloc(buffer, GCHandleType.Pinned);
         try {
            //Read in the structure at each position and coerce it into its slot
            T[] output = new T[Count];
            IntPtr current = pinned.AddrOfPinnedObject();
            for(int i = 0; i < Count; i++) {
               output[i] = (T)Marshal.PtrToStructure(current, typeof(T));
               current = (IntPtr)((int)current + Marshal.SizeOf(output[i])); //advance to the next index
            }
            return output;
#if DEBUG
         } catch(Exception ex) {
            Debug.WriteLine("ReadStruct: Unable to coerce foreign data into <" + typeof(T).ToString() + ">: " + ex.Message);
#else
         } catch {
#endif
         } finally {
            pinned.Free();
         }
      }
      return default(T[]);
   }

   /// <summary>
   /// Read an ANSI null-terminated string from the given address.
   /// </summary>
   /// <param name="Address">Address to begin reading</param>
   /// <param name="MaxLen">Maximum length of the string. This is the amount of memory to copy from the target process and should be oversized.</param>
   /// <returns>Returns a null-terminated string. If the target string is longer than MaxLen then the string will be truncated without the terminator.</returns>
   public string ReadString(IntPtr Address, int MaxLen) {
      byte[] buffer;
      int cnt = ReadMemory(Address, MaxLen, out buffer);
      if(cnt > 0) {
         GCHandle pinned = GCHandle.Alloc(buffer, GCHandleType.Pinned);
         try {
            return Marshal.PtrToStringAnsi(pinned.AddrOfPinnedObject());
#if DEBUG
         } catch(Exception ex) {
            Debug.WriteLine("ReadString: Unable to coerce foreign data into string: " + ex.Message);
#else
         } catch {
#endif
         } finally {
            pinned.Free();
         }
      }
      return "";
   }
   
   /// <summary>
   /// Convert the binary data at the offset from the base address into the specified data type or structure.
   /// </summary>
   /// <typeparam name="T">Type to coerce the data into</typeparam>
   /// <param name="Address">Address to begin reading</param>
   /// <returns>Returns the type filled with bytes from the requested address or a default type if the function fails.</returns>
   public T ReadOffsetStruct<T>(IntPtr Offset) {
      byte[] buffer;
      int cnt = ReadOffset(Offset, Marshal.SizeOf(typeof(T)), out buffer);
      if(cnt > 0) {
         GCHandle pinned = GCHandle.Alloc(buffer, GCHandleType.Pinned);
         try {
            return (T)Marshal.PtrToStructure(pinned.AddrOfPinnedObject(), typeof(T));
#if DEBUG
         } catch(Exception ex) {
            Debug.WriteLine("ReadOffsetStruct: Unable to coerce foreign data into <" + typeof(T).ToString() + ">: " + ex.Message);
#else
         } catch {
#endif
         } finally {
            pinned.Free();
         }
      }
      return default(T);
    }

   /// <summary>
   /// Searches the loaded process for the given byte signature within the specified module. Requires the BinarySearch library.
   /// </summary>
   /// <param name="Signature">The hex pattern to search for</param>
   /// <param name="Module">The module to focus the search within</param>
   /// <returns>The pointer found at the matching location</returns>
   public IntPtr FindSignature(string Signature, string Module) {
      return FindSignature(Signature, Module, 0);
   }

   /// <summary>
   /// Searches the loaded process for the given byte signature within the specified module. Requires the BinarySearch library.
   /// </summary>
   /// <param name="Signature">The hex pattern to search for</param>
   /// <param name="Module">The module to focus the search within</param>
   /// <param name="offset">An offset to add to the pointer VALUE</param>
   /// <returns>The pointer found at the matching location</returns>
   public IntPtr FindSignature(string Signature, string Module, int offset) {
      if(Signature.Length == 0 || Signature.Length % 2 != 0)
         throw new MemoryReaderException("Invalid signature");

      //Narrow the search breadth to only the requested module's address space
      MODULEENTRY32 info;
      if(GetModuleInfo(m_pid, Module, out info)) {
         //Take a memory snapshot of the entire module and then locate the pointer
         byte[] buffer;
         int cnt = ReadMemory((IntPtr)info.modBaseAddr, (int)info.modBaseSize, out buffer);
         if(cnt > 0) 
            return BinarySearch.FindSignature(buffer, Signature, offset);
      }
      return IntPtr.Zero;
   }
}

/// <summary>Custom exception class for catch filtering.</summary>
public sealed class MemoryReaderException : Exception {
   public MemoryReaderException(string message) : base(message) { }
   public MemoryReaderException(string message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// This is a debugging helper to facilitate off-site development.
///
/// <remarks>
/// This helper will take binary data from a source (such as a file dump) and insert it in at the
/// requested address in the CURRENT process. The goal is to allow realistic application development
/// when the target process cannot be ran on the development machine.
/// </remarks>
/// </summary>
public sealed class MemoryHelper {
   private static MemoryHelper m_instance;
   private List<IntPtr> m_alloclist = new List<IntPtr>();
   private List<IntPtr> m_pointerlist = new List<IntPtr>();

   [DllImport("kernel32.dll", SetLastError=true)]
   private static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocType flAllocationType, MemoryProtection flProtect);
   [DllImport("kernel32.dll", SetLastError=true)]
   private static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, DeallocType dwFreeType);
   
   private enum AllocType : uint {
      MEM_COMMIT = 0x1000,
      MEM_RESERVE = 0x2000,
      MEM_RESET = 0x80000,
      MEM_LARGE_PAGES = 0x20000000,
      MEM_PHYSICAL = 0x400000,
      MEM_TOP_DOWN = 0x100000,
      MEM_WRITE_WATCH = 0x200000
   }
   private enum MemoryProtection : uint {
      PAGE_EXECUTE = 0x10,
      PAGE_EXECUTE_READ = 0x20,
      PAGE_EXECUTE_READWRITE = 0x40,
      PAGE_EXECUTE_WRITECOPY = 0x80,
      PAGE_NOACCESS = 0x01,
      PAGE_READONLY = 0x02,
      PAGE_READWRITE = 0x04,
      PAGE_WRITECOPY = 0x08,
      PAGE_GUARD = 0x100,
      PAGE_NOCACHE = 0x200,
      PAGE_WRITECOMBINE = 0x400
   }
   private enum DeallocType : uint {
      MEM_DECOMMIT = 0x4000,
      MEM_RELEASE = 0x8000
   }

   [StructLayout(LayoutKind.Sequential, Pack = 1)]
   private struct DumpHeader {
      public IntPtr  StartAddress;
      public IntPtr  EndAddress;
      public int     Size;
   }

   //------------------------------------------------------------------
   // Singleton Controller
   //------------------------------------------------------------------
   private MemoryHelper() {} //MemoryHelper is a singleton; prevent arbitrary instantiation
   ~MemoryHelper() {
      //Ensure any allocated memory is released when the object is collected by the GC to prevent memory leaks
      foreach (IntPtr pAddress in m_alloclist) {
         if (pAddress != IntPtr.Zero)
            VirtualFree(pAddress, (UIntPtr)0, DeallocType.MEM_RELEASE);
      }
   }

   //------------------------------------------------------------------
   // Internal Functions
   //------------------------------------------------------------------
   private void CommitMemory(IntPtr BaseAddress, byte[] buffer) {
      try {
         //Allocate the requested memory location with the file length.
         IntPtr pMemory = VirtualAlloc(BaseAddress, (UIntPtr)buffer.Length, AllocType.MEM_COMMIT | AllocType.MEM_RESERVE, MemoryProtection.PAGE_READWRITE);
         if (pMemory == IntPtr.Zero) {
            #if VERBOSE
               Debug.WriteLine("CommitMemory: VirtualAlloc failed with error number " + Marshal.GetLastWin32Error());
            #endif
         } else {
            //Copy the file contents to the requested address
            // Note that pMemory will be padded to ne properly aligned and may or may not exactly match the requested address.
            Marshal.Copy(buffer, 0, BaseAddress, buffer.Length);
            m_pointerlist.Add(BaseAddress);
            m_alloclist.Add(pMemory);
         }
#if DEBUG
      } catch (Exception ex) {
            Debug.WriteLine("CommitMemory Exception: " + ex.Message);
#else
      } catch {
#endif
      }
   }

   private static T ReadStruct<T>(byte[] buffer, int offset) {
      GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
      try {
         T temp = (T)Marshal.PtrToStructure((IntPtr)((int)handle.AddrOfPinnedObject() + offset), typeof(T));
           return temp;
#if DEBUG
      } catch(Exception ex) {
            Debug.WriteLine("MemoryHelper ReadStruct: Unable to coerce local data into <" + typeof(T).ToString() + ">: " + ex.Message);
#else
      } catch {
#endif
      } finally {
           handle.Free();
      }
      return default(T);
    }

   private static MemoryHelper Instance {
      get {
         if (m_instance == null)
            m_instance = new MemoryHelper();
         return m_instance;
      }
   }

   //------------------------------------------------------------------
   // Public Accessors
   //------------------------------------------------------------------
   /// <summary>
   /// Gets the current list of committed addresses
   /// </summary>
   public static System.Collections.ObjectModel.ReadOnlyCollection<IntPtr> AddressList {
      get { return Instance.m_pointerlist.AsReadOnly(); }
   }
   /// <summary>
   /// Writes a type or structure into memory
   /// </summary>
   /// <typeparam name="T">Type of the source data</typeparam>
   /// <param name="BaseAddress">Address to begin writing</param>
   /// <param name="data">Source data</param>
   public static void WriteStruct<T>(IntPtr BaseAddress, T data) {
      try {
         IntPtr pData = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
         try {
            //Convert the structure to a byte array
            Marshal.StructureToPtr(data, pData, true);
            byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
            Marshal.Copy(pData, buffer, 0, buffer.Length);
            //Write the data
            Instance.CommitMemory(BaseAddress, buffer);
#if DEBUG
         } catch (Exception ex) {
               Debug.WriteLine("WriteStruct Exception: " + ex.Message);
#else
         } catch {
#endif
         } finally {
            Marshal.FreeHGlobal(pData);
         }
#if DEBUG
      } catch (Exception ex) {
            Debug.WriteLine("WriteStruct Exception: " + ex.Message);
#else
      } catch {
#endif
      }
   }
   /// <summary>
   /// Writes a byte array into memory
   /// </summary>
   /// <param name="BaseAddress">Address to begin writing</param>
   /// <param name="data">Binary data</param>
   public static void WriteData(IntPtr BaseAddress, byte[] data) {
      Instance.CommitMemory(BaseAddress, data);
   }
   /// <summary>
   /// Writes a raw dump file into memory
   /// </summary>
   /// <param name="BaseAddress">Address to begin writing</param>
   /// <param name="FileName">File name and path to the raw binary data</param>
   public static void WriteFile(IntPtr BaseAddress, string FileName) {
      try {
         //Load the file into a buffer
         FileStream stream = File.OpenRead(FileName);
         byte[] buffer = new byte[stream.Length];
         int readcount = stream.Read(buffer, 0, buffer.Length);
         if (readcount != stream.Length) {
            #if DEBUG
               Debug.WriteLine("WriteFile Error: Read count != File size!");
            #endif
            return;
         }
         stream.Close();
         Instance.CommitMemory(BaseAddress, buffer);
#if DEBUG
      } catch (Exception ex) {
            Debug.WriteLine("WriteFile Exception: " + ex.Message);
#else
      } catch {
#endif
      }
   }
   /// <summary>
   /// Writes a TSearch dump file into memory
   /// </summary>
   /// <param name="FileName">File name and path to the TSearch dump</param>
   public static void WriteDump(string FileName) {
      try {
         //Load the file into a buffer
         FileStream stream = File.OpenRead(FileName);
         byte[] buffer = new byte[stream.Length];
         int readcount = stream.Read(buffer, 0, buffer.Length);
         if (readcount != stream.Length) {
            #if DEBUG
               Debug.WriteLine("WriteDump Error: Read count != File size!");
            #endif
            return;
         }
         stream.Close();

         DumpHeader header = ReadStruct<DumpHeader>(buffer, 0);

         int headerlen = Marshal.SizeOf(typeof(DumpHeader));
         if (header.Size == buffer.Length - headerlen) {
            byte[] contents = new byte[buffer.Length - headerlen];
            Array.Copy(buffer, headerlen, contents, 0, buffer.Length - headerlen);
            Instance.CommitMemory(header.StartAddress, contents);
         } else {
            #if DEBUG
               Debug.WriteLine("WriteDump Error: File is not a TSearch dump.");
            #endif
         }
#if DEBUG
      } catch (Exception ex) {
            Debug.WriteLine("WriteDump Exception: " + ex.Message);
#else
      } catch {
#endif
      }
   }
}
