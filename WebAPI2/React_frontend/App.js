import React from "react";
import { Container, Typography, Paper } from "@mui/material";
import DepartmentList from "./components/DepartmentList";
import DepartmentForm from "./components/DepartmentForm";

function App() {
  const [reload, setReload] = React.useState(false);

   return (
    <Container maxWidth="md" style={{ marginTop: "40px" }}>
      <Paper elevation={3} style={{ padding: "20px" }}>
        <Typography variant="h4" align="center" gutterBottom>
          Department Management
        </Typography>

        <DepartmentForm onAdded={() => setReload(!reload)} />
        <DepartmentList key={reload} />
      </Paper>
    </Container>
  );
}

export default App;
