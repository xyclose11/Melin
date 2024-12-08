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

const checkRole = async () => {
    return await instance.get(`api/Auth/user-role`, { withCredentials: true });
};

interface AuthContextType {
    isAuthenticated: boolean;
    setIsAuthenticated: (value: boolean) => void;
    userRole: string;
    setUserRole: (value: string) => void;
}

enum Roles {
    User = "USER",
    Admin = "ADMIN",
    Guest = "GUEST",
    Moderator = "MODERATOR",
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
            const response = await checkRole();
            // Role is in the 0th index position because the application currently only supports a single Role per user
            const role = response.data[0].toUpperCase();
            // verify User Role
            if (Object.values(Roles).includes(role)) {
                setUserRole(role);
            } else {
                console.error("USER ROLE INVALID: ", response);
                // TODO implement logging
            }
        } catch (error) {
            console.error("Failed to retrieve user role: ", error);
            // Default role to "Guest" to prevent privilege escalation
            setUserRole("Guest");
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

    const handleSetUserRole = (value: string) => {
        setUserRole(value);
    };

    return (
        <AuthContext.Provider
            value={{
                isAuthenticated,
                setIsAuthenticated: handleSetIsAuthenticated,
                userRole,
                setUserRole: handleSetUserRole,
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
