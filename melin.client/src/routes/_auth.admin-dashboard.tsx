import { createFileRoute } from "@tanstack/react-router";
import { useAuth } from "@/utils/AuthProvider.tsx";

export const Route = createFileRoute("/_auth/admin-dashboard")({
    component: RouteComponent,
});

function RouteComponent() {
    const auth = useAuth();
    return <div>Hello "/admin-dashboard"!</div>;
}
