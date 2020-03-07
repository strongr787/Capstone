using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    /// <summary>
    /// A Setting is an object representing a configurable option for the application
    /// </summary>
    class Setting
    {
        public int SettingID { get; set; }
        public string DisplayName { get; set; }
        public List<SettingOption> Options { get; set; } = new List<SettingOption>();

        public Setting() : this(-1, "", new List<SettingOption> { })
        {

        }


        public Setting(int SettingID, string DisplayName, List<SettingOption> Options)
        {
            this.SettingID = SettingID;
            this.DisplayName = DisplayName;
            this.Options = Options;
        }
    }

    class SettingOption
    {
        public int OptionID { get; set; }
        public string DisplayName { get; set; }
        public bool IsSelected { get; set; }

        public SettingOption() : this(-1, "", false)
        {

        }

        public SettingOption(int OptionID, string DisplayName, bool IsSelected)
        {
            this.OptionID = OptionID;
            this.DisplayName = DisplayName;
            this.IsSelected = IsSelected;
        }
    }
}
