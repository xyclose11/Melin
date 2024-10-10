import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthProvider"; // Adjust the import path based on your structure

const PrivateRoute = ({ element }: { element: JSX.Element }) => {
    const { isAuthenticated } = useAuth(); // Assuming you have an AuthContext
    return isAuthenticated ? element : <Navigate to="/login" />;
};

export default PrivateRoute;
