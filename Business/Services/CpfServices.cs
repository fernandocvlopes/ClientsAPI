using System;
using System.Linq;

namespace ClientsAPI.Business.Services
{
    public class CpfServices
    {
        private string UnformatCpf(string Cpf)
        {
            // Removes the format chars of Cpf and empty spaces
            return Cpf.Replace(".", string.Empty).Replace("-", string.Empty).Trim();
        }

        private bool IsNumeric(string cpf)
        {
            UInt64 v;
            return UInt64.TryParse(cpf, out v);
        }

        public string FormatCpf(string cpf)
        {
            return Convert.ToUInt64(this.UnformatCpf(cpf)).ToString(@"000\.000\.000\-00");
        }

        public bool ValidateCpf(string Cpf)
        {
            Cpf = UnformatCpf(Cpf);

            if (!IsNumeric(Cpf))
                return false;
            else if (Cpf.Length != 11)
                return false;

            int[] cpf = Cpf.Select(c => c - '0').ToArray();

            // The CPF validation bellow was taken from https://en.wikipedia.org/wiki/Cadastro_de_Pessoas_F%C3%ADsicas and translated to C#

            var v = new int[2];

            //Compute 1st verification digit.
            v[0] = 1 * cpf[0] + 2 * cpf[1] + 3 * cpf[2];
            v[0] += 4 * cpf[3] + 5 * cpf[4] + 6 * cpf[5];
            v[0] += 7 * cpf[6] + 8 * cpf[7] + 9 * cpf[8];
            v[0] = v[0] % 11;
            v[0] = v[0] % 10;

            //Compute 2nd verification digit.
            v[1] = 1 * cpf[1] + 2 * cpf[2] + 3 * cpf[3];
            v[1] += 4 * cpf[4] + 5 * cpf[5] + 6 * cpf[6];
            v[1] += 7 * cpf[7] + 8 * cpf[8] + 9 * v[0];
            v[1] = v[1] % 11;
            v[1] = v[1] % 10;

            //True if verification digits are as expected.
            return v[0] == cpf[9] && v[1] == cpf[10];
        }
    }
}
