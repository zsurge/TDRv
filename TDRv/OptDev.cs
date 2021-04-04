using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDRv.Driver;

namespace TDRv
{
    public sealed class OptDev
    {
        E5080B analyzer = new E5080B();

        private static readonly Lazy<OptDev> lazy =
        new Lazy<OptDev>(() => new OptDev());

        public static OptDev Instance { get { return lazy.Value; } }

        private OptDev()
        {
        }

        public int OpenDev(string devString)
        {
            int error = analyzer.Open(devString);
            string idn = string.Empty;
            error = analyzer.GetInstrumentIdentifier(out idn);
            return error;
        }

        public int GetDevInitIndexValue()
        {
            int error = 0;

            string cmd2 = "FORM:DATA ASCII";
            analyzer.ExecuteCmd(cmd2);

            string cmd3 = "MMEM:STOR:TRAC:FORM:SNP MA";
            analyzer.ExecuteCmd(cmd3);

            return error;
        }
    }
}


