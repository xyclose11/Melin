import { instance } from "@/utils/axiosInstance.ts";
import { useNavigate } from "@tanstack/react-router";
import { Button } from "@/components/ui/button.tsx";
import { useToast } from "@/hooks/use-toast.ts";
import { useAuth } from "@/utils/AuthProvider.tsx";
import { useQueryClient } from "@tanstack/react-query";

export default function LogoutLink() {
    const { toast } = useToast();
    const navigate = useNavigate();
    const { setIsAuthenticated } = useAuth();
    const queryClient = useQueryClient();

    const logout = async (e: any) => {
        e.preventDefault();
        try {
            const res = await instance.post(
                `/api/auth/logout`,
                {},
                { withCredentials: true },
            );

            if (res.status === 200) {
                setIsAuthenticated(false);

                // queryClient.removeQueries();
                await queryClient.refetchQueries();
                queryClient.clear();

                toast({
                    title: "Successfully Logged Out",
                    description: `User logged out`,
                });
                await navigate({ to: "/login" });
            } else {
                toast({
                    variant: "destructive",
                    title: "Unable to Log Out",
                    description: `Unable to log user out. Please try again later`,
                });
            }
        } catch (error) {
            console.error("Logout failed:", error);
        }
    };

    return (
        <form
            onSubmit={logout}
            className="text-muted-foreground transition-colors hover:text-foreground"
        >
            <Button type="submit">Logout</Button>
        </form>
    );
}
