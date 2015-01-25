using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hex {
    public partial class frmDataInspector : Form
    {
        private DataView _dataView;

        public DataView DataView
        {
            get { return _dataView; }
            set
            {
                _dataView = value;
                UpdateView();
            }
        }

        public void UpdateView()
        {
            propertyGrid1.SelectedObject = _dataView;
        }

        public frmDataInspector() {
            InitializeComponent();
        }

    }

    public class DataView
    {
        private readonly byte[] _buffer;
        [Browsable(false)]
        public int Offset { get; set; }

        public byte Byte
        {
            get { return _buffer[Offset]; }
        }

        public Int16 SignedWord {
            get { return BitConverter.ToInt16(_buffer, Offset); }
        }

        public UInt16 UnsignedWord {
            get { return BitConverter.ToUInt16(_buffer, Offset); }
        }

        public Int32 SignedDWord {
            get { return BitConverter.ToInt32(_buffer, Offset); }
        }

        public UInt32 UnsignedDWord {
            get { return BitConverter.ToUInt32(_buffer, Offset); }
        }

        public float Single {
            get { return BitConverter.ToSingle(_buffer, Offset); }
        }

        public double Double {
            get { return BitConverter.ToDouble(_buffer, Offset); }
        }

        public DataView(byte[] buffer)
        {
            _buffer = buffer;
        }
    }
}
