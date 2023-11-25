import { Card } from "@mui/material";
import SoftInput from "components/SoftInput";
import BasicLayout from "layouts/authentication/components/BasicLayout";
import DashboardLayout from "examples/LayoutContainers/DashboardLayout";

function BlankPageLayout() {
    return (
        <DashboardLayout>
            <SoftInput/>
        </DashboardLayout>
    );
  }
  
  export default BlankPageLayout;