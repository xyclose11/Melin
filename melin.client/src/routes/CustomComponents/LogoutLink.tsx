import { instance } from "@/utils/axiosInstance.ts";
import { Form, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button.tsx";
import { useToast } from "@/hooks/use-toast.ts";

export default function LogoutLink() {
    const { toast } = useToast();
    const navigate = useNavigate();

    const logout = async (e: any) => {
        e.preventDefault();
        try {
            const res = await instance.post(
                `/api/auth/logout`,
                {},
                { withCredentials: true },
            );

            if (res.status === 200) {
                navigate("/login");
                toast({
                    title: "Successfully Logged Out",
                    description: `User logged out`,
                });
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
        <Form
            onSubmit={logout}
            className="text-muted-foreground transition-colors hover:text-foreground"
        >
            <Button type="submit">Logout</Button>
        </Form>
    );
}
