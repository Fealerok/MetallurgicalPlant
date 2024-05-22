namespace MetallurgicalPlantLibrary
{
    public class CountingProducts
    {
        public static int GetValueOfProducts(int valueOfResourses, string typeOfResourses)
        {
            string value = "";
            switch (typeOfResourses)
            {
                case "KP80":
                    value = (valueOfResourses / 15).ToString();
                    break;
                case "KP90":
                    value = (valueOfResourses / 21).ToString();
                    break;
                case "KP100":
                    value = (valueOfResourses / 11).ToString();
                    break;
            }

            if (value.Contains('.'))
            {
                return int.Parse(value.Split(value[value.IndexOf('.')])[0]);

            }
            else
            {
                return int.Parse(value);
            }
        }

        

        
    }
}
