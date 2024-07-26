

using System.Text;

namespace FinalProject.Core.Application.Utils.CodeGenerator
{
    public static class PropertyCodeGenerator
    {
        public static string GeneratePropertyCode()
        {
            StringBuilder stringBuilder = new();
            Random random = new();
            for(int i = 0; i<6; i++)
            {
                stringBuilder.Append(random.Next(9));
            }
            return stringBuilder.ToString();
        }
    }
}
