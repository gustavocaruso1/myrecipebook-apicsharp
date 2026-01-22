using MyRecipeBook.Application.Services.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new PasswordEncripter("senha123");
    }
}
