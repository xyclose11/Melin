import { Navigate } from "@tanstack/react-router";
import { useAuth } from "./AuthProvider"; // Adjust the import path based on your structure

const PrivateRoute = ({ element }: { element: JSX.Element }) => {
    const { isAuthenticated } = useAuth();
    return isAuthenticated ? element : <Navigate to="/login" />;
};

export const AdminRoute = ({ element }: { element: JSX.Element }) => {
    const { userRole } = useAuth();
    return userRole.toUpperCase() === "ADMIN" ? element : <Navigate to={"/"} />;
};

export default PrivateRoute;
