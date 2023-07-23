using MainProject.Model;

namespace MainProject.CommonHelper
{
    public class ProcessInput
    {
        public async static Task<Employee> GetEmployee(string data)
        {
            string[] dataLines = data.Split(Environment.NewLine);
            string fieldLine = dataLines.FirstOrDefault();
            string valueLine = dataLines.Skip(1).FirstOrDefault();
            string[] fields = fieldLine.Split('/');
            string[] values = valueLine.Split('/');
            Employee employee = new Employee
            {
                FirstName = values[0],
                LastName = values[1],
                BasicSalery = long.Parse(values[2]),
                Allowance = long.Parse(values[3]),
                Transportation = long.Parse(values[4]),
                Date = DateTime.ParseExact(values[5], "yyMMdd", null)
            };
            return employee;
        }
    }
}
