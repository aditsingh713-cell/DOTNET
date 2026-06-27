import React, { useEffect, useState } from "react";
import { motion } from "framer-motion";
import { Table,  TableBody,  TableCell,  TableContainer,  TableHead,  TableRow,  Paper,} from "@mui/material";
import { Modal, Box, Typography } from "@mui/material";
import { getDepartmentLogs } from "../services/departmentService";
import { getDepartments} from "../services/departmentService";
import { Button } from "@mui/material";

function DepartmentList() {
  const [departments, setDepartments] = useState([]);

  useEffect(() => {
    loadDepartments();
  }, []);

  const [open, setOpen] = useState(false);
const [logs, setLogs] = useState([]);
const [selectedDept, setSelectedDept] = useState("");
  const loadDepartments = async () => {
    const response = await getDepartments();
    setDepartments(response.data);
  };

 const handleOpenLogs = async () => {
  const response = await getDepartmentLogs();
  setLogs(response.data);
  setOpen(true);
};


  return (
    
     <>
    
    <TableContainer component={Paper} sx={{ mt: 3 }}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell><strong>ID</strong></TableCell>
            <TableCell><strong>Department Name</strong></TableCell>
           
          </TableRow>
        </TableHead>

        <TableBody>
  {departments.map((d, index) => (
    <motion.tr
      key={d.departmentID}
      initial={{ opacity: 0, x: -20 }}
      animate={{ opacity: 1, x: 0 }}
      transition={{ delay: index * 0.1 }}
      style={{ display: "table-row" }}
    >
      <td>{d.departmentID}</td>
      <td>{d.departmentName}</td>
      <td>
        {/* delete button */}
      </td>
    </motion.tr>
  ))}
  

</TableBody>



      </Table>
    </TableContainer>
    <Modal open={open} onClose={() => setOpen(false)}>
  <Box sx={{
    position: "absolute",
    top: "50%",
    left: "50%",
    transform: "translate(-50%, -50%)",
    width: 500,
    bgcolor: "background.paper",
    boxShadow: 24,
    p: 4,
    borderRadius: 2
  }}>
    <Typography variant="h6" gutterBottom>
      Logs for {selectedDept}
    </Typography>

    {logs.length === 0 ? (
      <Typography>No logs found.</Typography>
    ) : (
      logs.map((log) => (
        <Box key={log.logID} sx={{ mb: 2, p: 1, borderBottom: "1px solid #ccc" }}>
          <Typography><strong>Action:</strong> {log.actionName}</Typography>
          <Typography><strong>Time:</strong> {log.timestamp}</Typography>
        </Box>
      ))
    )}
  </Box>
</Modal>
</>
  );
}

export default DepartmentList;
