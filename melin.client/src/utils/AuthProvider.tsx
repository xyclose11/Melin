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

    const checkUserAuth = async () => {
        try {
            const response = await checkAuth();
            const isAuthenticated = response.data; // Ensure response.data is a boolean
            setIsAuthenticated(isAuthenticated);
            // Store authentication state in localStorage
            localStorage.setItem("isAuthenticated", JSON.stringify(isAuthenticated));
        } catch (error) {
            console.error("Failed to check authentication:", error);
            setIsAuthenticated(false); // Handle the error case
            localStorage.removeItem("isAuthenticated"); // Clear on error
        }
    };

    useEffect(() => {
        checkUserAuth();
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
        <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated: handleSetIsAuthenticated }}>
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
