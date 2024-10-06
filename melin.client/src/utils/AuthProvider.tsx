import { createContext, useContext, useState, useEffect } from "react";
import { instance } from "@/utils/axiosInstance.ts";

const AuthContext = createContext("");
const checkAuth = async () => {
    return await instance.get(`api/Auth/check`);
};
// @ts-ignore
export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    const checkUserAuth = async () => {
        const response = await checkAuth();
        setIsAuthenticated(response.data);
    };

    useEffect(() => {
        checkUserAuth();
    }, []);

    return (
        // @ts-ignore
        <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
