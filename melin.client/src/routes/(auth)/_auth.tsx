import { createFileRoute, Outlet, redirect } from "@tanstack/react-router";

export const Route = createFileRoute("/(auth)/_auth")({
    beforeLoad: async ({ context, location }) => {
        if (!context.auth.isAuthenticated) {
            throw redirect({
                to: "/login",
                search: {
                    redirect: location.href,
                },
            });
        }
    },
    component: AuthLayout,
});

function AuthLayout() {
    return (
        <div className="h-screen mt-24">
            <Outlet />
        </div>
    );
}
