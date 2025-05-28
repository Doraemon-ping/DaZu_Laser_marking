using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using License = Portable.Licensing.License;


namespace DaZu_Laser_marking
{
    public class Success
    {
        private static readonly string publicKey = "MIIBKjCB4wYHKoZIzj0CATCB1wIBATAsBgcqhkjOPQEBAiEA/////wAAAAEAAAAAAAAAAAAAAAD///////////////8wWwQg/////wAAAAEAAAAAAAAAAAAAAAD///////////////wEIFrGNdiqOpPns+u9VXaYhrxlHQawzFOw9jvOPD4n0mBLAxUAxJ02CIbnBJNqZnjhE50mt4GffpAEIQNrF9Hy4SxCR/i85uVjpEDydwN9gS3rM6D0oTlF2JjClgIhAP////8AAAAA//////////+85vqtpxeehPO5ysL8YyVRAgEBA0IABDB2bTmms+vmGnCvg/IKXxouFcEbnk/rfC0HT3NUPbPj/BF3T9L7qT211GA6efp9LNS9nt5SmcDHqctkIYyJuCA="; // 可以写死在程序中
        public static bool cheek() {
            try
            {
                string licenseText = File.ReadAllText("license.lic");
                var license = License.Load(licenseText);
                var result = license.VerifySignature(publicKey);
                // .SignWithPublicKey(publicKey)
                //.AssertValidLicense(); // 抛出异常则非法
                // 可进一步检查是否过期或绑定信息是否匹配
                if (license.Expiration < DateTime.UtcNow)
                {
                    MessageBox.Show("许可证已过期。");
                    Environment.Exit(0);
                    return false;
                }
                if (result == false)
                {
                    MessageBox.Show("许可证不正确！");
                    Environment.Exit(0);
                    return false;
                }
                else {
                    return result;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
                return false;
            }
        }
    }
}
