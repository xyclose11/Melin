import { instance } from "@/utils/axiosInstance.ts";
import { Form } from "react-router-dom";
import { Button } from "@/components/ui/button.tsx";

export default function LogoutLink() {
    const logout = async (e: any) => {
        e.preventDefault();
        try {
            await instance
                .post(`/api/auth/logout`)
                // TODO CHANGE THE ABOVE URL TO BE DYNAMIC FOR THE SERVER IT IS SET ON
                .then(function (response) {
                    console.log(response);
                });
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
