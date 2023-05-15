using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO;

namespace DAL.DTO
{
    public class TaskDTO : EmployeeContext
    {
        public List<EmployeeDetailDTO>  Employees { get; set; }

        public List<DEPARTMENT> Departments { get; set; }
        public List<PositionDTO> Positions { get; set; }

        public List<TASKSTATE> TaskStates { get; set; }

        public List<TaskDetailDTO> Tasks { get; set; }

        public static List<TaskDetailDTO> getTasks()
        {
            List<TaskDetailDTO> tasklist = new List<TaskDetailDTO>();
            var list = (
                    from t in db.TASKs
                    join s in db.TASKSTATEs on t.TaskState equals s.ID
                    join e in db.EMPLOYEEs on t.EmployeeID equals e.ID
                    join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                    join p in db.POSITIONs on e.PositionID equals p.ID
                    select new
                    {
                        taskID = t.ID,
                        title = t.TaskTitle,
                        content = t.TaskContent,
                        startdate = t.TaskStartDate,
                        deliveryDate = t.TaskDeliveryDate,
                        taskStatenName = s.StateName,
                        taskStateID = t.TaskState,
                        UserNo = e.UserNo,
                        Name = e.Name,
                        EmployeeId = t.EmployeeID,
                        Surname = e.Surname,
                        positionName = p.PositionName,
                        departmentName = d.DepartmentName,
                        positionID = e.PositionID,
                        departmentId = e.DepartmentID

                    }
                ).OrderBy(x=>x.startdate).ToList();

            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.content = item.content;
                dto.TaskStartDate = item.startdate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStatenName;
                dto.taskStateID = item.taskStateID;
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;   
                dto.Surname = item.Surname;
                dto.DepartmentName = item.departmentName;
                dto.PositionID = item.positionID;
                dto.PositionName = item.positionName;
                dto.EmployeeID = item.EmployeeId;
                tasklist.Add(dto);
            }
            return tasklist;
        }
    }
}
