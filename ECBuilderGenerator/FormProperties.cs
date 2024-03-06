using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ECBuilderGenerator
{
    public partial class FormProperties : Form
    {
        public FormProperties(PropertyInfo[] propertyInfos)
        {
            InitializeComponent();
            PropertyInfos = propertyInfos;
        }

        private PropertyInfo[] PropertyInfos { get; set; }

        private Type[] types = new Type[]
        {
            typeof(string),
            typeof(int),
        };

        private void FormProperties_Load(object sender, EventArgs e)
        {
            foreach (PropertyInfo propertyInfo in PropertyInfos)
            {
                Type type = types.FirstOrDefault(ftype => (propertyInfo.PropertyType.IsEquivalentTo(ftype) || propertyInfo.PropertyType.IsEnum) && propertyInfo.CanWrite);
                if (type == null) continue;

                dataGridViewProperties.Rows.Add(propertyInfo.Name, propertyInfo.GetValue);
            }
        }
    }
}
