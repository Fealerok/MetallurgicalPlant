using MetallurgicalPlantLibrary;

namespace CalculatingProductsTest
{
    [TestClass]
    public class CalculatingTest
    {

        /// <summary>
        /// ����� ��� ������������
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //����������� ������
            int valueOfResourses = 10;
            string typeOfResourses = "KP80";

            //��������� ���������
            int expected = valueOfResourses / 15;

            //����������� ���������
            int result = CountingProducts.GetValueOfProducts(valueOfResourses, typeOfResourses);

            //�������� �� �������� ���������� � ������������ ����������
            Assert.AreEqual(expected, result);
        }
    }
}