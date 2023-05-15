using DAL;
using System;
using DAL.DAO;
using System.Collections.Generic;
using DAL.DTO;

namespace BLL
{
    public class PositionBLL
    {
        public static void AddPosition(POSITION position)
        {
            PositionDAO.AddPosition(position);
        }

        public static void DeletePosition(int iD)
        {
            PositionDAO.DeletePosition(iD);
        }

        public static List<PositionDTO> GetPositions()
        {
            return PositionDAO.GetPositions();
        }

        public static void UpdatePosition(POSITION position, bool control)
        {
            PermissionDAO.UpdatePosition(position);
            if (control)
                EmployeeDAO.UpdateEmployee(position);
        }
    }
}
