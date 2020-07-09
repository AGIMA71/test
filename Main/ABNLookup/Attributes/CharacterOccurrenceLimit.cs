using System.ComponentModel.DataAnnotations;

namespace ABNLookup.Attributes
{
    public class CharacterOccurrenceLimit : ValidationAttribute
    {
        public int OccurrenceLimit { get; set; }
        public string CharacterList { get; set; }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                foreach (char c in CharacterList)
                {
                    int count = 0;
                    foreach (char s in value as string)
                    {
                        if (s == c) count++;
                    }
                    if (count > OccurrenceLimit) return false;
                }
            }
            return true;
        }
    }
}
