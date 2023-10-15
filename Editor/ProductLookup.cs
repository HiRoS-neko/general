using UnityEditor;

namespace Devdog.General.Editors
{
    public class ProductLookup
    {
        private const string RatingPrefixName = "Devdog_Product_Rating_";

        public string email = "";
        public string feedback = "";
        public string name;
        public string niceName;

        public bool hasReview => rating != null;

        public int? rating
        {
            get
            {
                if (EditorPrefs.HasKey(RatingPrefixName + name)) return EditorPrefs.GetInt(RatingPrefixName + name, 0);

                return null;
            }
            set
            {
                if (value == null)
                    EditorPrefs.DeleteKey(RatingPrefixName + name);
                else
                    EditorPrefs.SetInt(RatingPrefixName + name, value.GetValueOrDefault(0));
            }
        }

        public override string ToString()
        {
            return niceName;
        }
    }
}