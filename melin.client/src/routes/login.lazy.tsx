import { createLazyFileRoute } from "@tanstack/react-router";
import { LoginForm } from "@/Login.tsx";

export const Route = createLazyFileRoute("/login")({
    component: LoginForm,
});
