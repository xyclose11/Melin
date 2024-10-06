import {
    createContext,
    useContext,
    useState,
    useEffect,
    ReactNode,
} from "react";
import { instance } from "@/utils/axiosInstance.ts";

const checkAuth = async () => {
    return await instance.get(`api/Auth/check`);
};

interface AuthContextType {
    isAuthenticated: boolean;
    setIsAuthenticated: (value: boolean) => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({
    children,
}) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const checkUserAuth = async () => {
        try {
            const response = await checkAuth();
            console.log(response);
            setIsAuthenticated(response.data); // Ensure response.data is a boolean
        } catch (error) {
            console.error("Failed to check authentication:", error);
            setIsAuthenticated(false); // Handle the error case
        }
    };

    useEffect(() => {
        checkUserAuth();
    }, []);

    return (
        <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error("useAuth must be used within an AuthProvider");
    }
    return context;
};
