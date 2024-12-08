import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";

const PrivateRoute = ({ element }: { element: JSX.Element }) => {
    const { isAuthenticated } = useAuth();
    return isAuthenticated ? element : <Navigate to="/login" />;
};

export const AdminRoute = ({ element }: { element: JSX.Element }) => {
    const { userRole } = useAuth();
    return userRole.toUpperCase() === "ADMIN" ? element : <Navigate to={"/"} />;
};

export default PrivateRoute;
