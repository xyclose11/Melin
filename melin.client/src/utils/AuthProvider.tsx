import {
    createContext,
    useContext,
    useState,
    useEffect,
    ReactNode,
} from "react";
import { instance } from "@/utils/axiosInstance.ts";

const checkAuth = async () => {
    return await instance.get(`api/Auth/check`, { withCredentials: true });
};

interface AuthContextType {
    isAuthenticated: boolean;
    setIsAuthenticated: (value: boolean) => void;
    userRole: string;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({
    children,
}) => {
    const [isAuthenticated, setIsAuthenticated] = useState(() => {
        // Check localStorage for stored authentication state on initial load
        const storedAuthState = localStorage.getItem("isAuthenticated");
        return storedAuthState ? JSON.parse(storedAuthState) : false;
    });

    const [userRole, setUserRole] = useState("Guest");

    const checkUserAuth = async () => {
        try {
            const response = await checkAuth();
            const isAuthenticated = response.data; // Ensure response.data is a boolean
            setIsAuthenticated(isAuthenticated);
            // Store authentication state in localStorage
            localStorage.setItem(
                "isAuthenticated",
                JSON.stringify(isAuthenticated),
            );
        } catch (error) {
            console.error("Failed to check authentication:", error);
            setIsAuthenticated(false); // Handle the error case
            localStorage.removeItem("isAuthenticated"); // Clear on error
        }
    };

    const checkUserRole = async () => {
        try {
            let res = await instance.get("api/Auth/user-role", {
                withCredentials: true,
            });
            if (res.status === 200) {
                setUserRole(res.data[0]);
            } else {
                // setting User Role to guest on response fail to ensure user cannot access
                // sensitive pages if auth server is down
                setUserRole("Guest");
            }
        } catch (e) {
            console.error("Failed to retrieve users role", e);
        }
    };

    useEffect(() => {
        checkUserAuth();
        checkUserRole();
    }, []);

    const handleSetIsAuthenticated = (value: boolean) => {
        setIsAuthenticated(value);
        // Update localStorage whenever authentication state changes
        if (value) {
            localStorage.setItem("isAuthenticated", JSON.stringify(value));
        } else {
            localStorage.removeItem("isAuthenticated");
        }
    };

    return (
        <AuthContext.Provider
            value={{
                isAuthenticated,
                setIsAuthenticated: handleSetIsAuthenticated,
                userRole,
            }}
        >
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
