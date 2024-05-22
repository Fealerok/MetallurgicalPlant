using MetallurgicalPlantLibrary;

namespace CalculatingProductsTest
{
    [TestClass]
    public class CalculatingTest
    {

        /// <summary>
        /// Метод для тестирования
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //Тестируемые данные
            int valueOfResourses = 10;
            string typeOfResourses = "KP80";

            //Ожидаемый результат
            int expected = valueOfResourses / 15;

            //Фактический результат
            int result = CountingProducts.GetValueOfProducts(valueOfResourses, typeOfResourses);

            //Проверка на равность ожидаемого и фактического результата
            Assert.AreEqual(expected, result);
        }
    }
}