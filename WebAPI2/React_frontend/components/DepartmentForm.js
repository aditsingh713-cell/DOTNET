import React, { useState } from "react";
import { TextField, Button, Box } from "@mui/material";
import { addDepartment } from "../services/departmentService";
import { motion } from "framer-motion";

function DepartmentForm({ onAdded }) {
  const [departmentName, setDepartmentName] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    await addDepartment({ departmentName });

    setDepartmentName("");
    onAdded();
  };

  return (
    <motion.div
      initial={{ opacity: 0, y: -20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.5 }}
    >
      <Box component="form" onSubmit={handleSubmit} sx={{ mb: 3 }}>
        <TextField
          label="Department Name"
          variant="outlined"
          fullWidth
          value={departmentName}
          onChange={(e) => setDepartmentName(e.target.value)}
          sx={{ mb: 2 }}
        />

        <motion.div whileHover={{ scale: 1.05 }} whileTap={{ scale: 0.95 }}>
          <Button variant="contained" color="primary" type="submit" fullWidth>
            Add Department
          </Button>
        </motion.div>
      </Box>
    </motion.div>
  );
}

export default DepartmentForm;
