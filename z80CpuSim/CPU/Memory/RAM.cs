using System;
using System.Collections.Generic;
using System.Text;

namespace z80CpuSim.CPU.Memory
{
    /** RAM class, inherits memory interface
     * 
     * Contains a byte array for data, and methods to read and write to addresses.
     * RAM also specifically contains a setup method, this sets the data array to a prepopulated
     * state as supplied, there is no extra storage, the entire of RAM is considered the full 
     * storage for the simulation.
     * 
     **/ 
    class RAM : IMemory
    {
        // maximum size is 65536
        private byte[] data;

        // Empty constructor, initialise a completely empty bit of RAM, pretty useless, but its here in case you dont need to run setup (for any reason?)
        public RAM()
        {
            
        }

        // Constructor that is probably going to be most useful, this creates a new RAM (english broken there, though, i guess its technically correct for this class)
        // of the specified size (maximum 65535 (i should probably enforce this somewhere, but not here)) and puts data in it from the 0th index, data can technically be an empty array
        // so this constructor almost serves the same purpose as above.
        public RAM(int size, byte[] data) 
        {
            SetSize(size);

            // This can throw and exception, its handled within the setup method however, once i write it, it will throw a general exception allowing me to catch it and display an error message
            Setup(data);

        }


        public byte GetAddress(ushort addr)
        {
            return data[addr];
        }

        public void SetAddress(ushort addr, byte data)
        {
            this.data[addr] = data;
        }

        // Initialise the array to the specified size, the array will be initialised to 0s. Now, in reality, this may not be the case, but for the sake of this, it is.
        // This really should be run when initialising the memory
        public void SetSize(int size)
        {
            data = new byte[size];
        }

        // Fills the memory with hte supplied data, this can also be run from the constructor
        public void Setup(byte[] data)
        {
            try
            {
                for (int i = 0; i < data.Length; i++)
                {
                    this.data[i] = data[i];
                }
            } catch (IndexOutOfRangeException e)
            {
                // Mot enough memory to complete the writing, the memory needs to be larger
            }
            
        }

        public byte[][] ToArray2D()
        {
            byte[][] a2d = new byte[256][];

            for (int i = 0; i < 256; i++ )
            {
                for (int j = 0; j < 256; j++)
                {
                    a2d[i][j] = data[i * j];
                }
            }

            return a2d;
        }

    }
}
