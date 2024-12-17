import { createLazyFileRoute } from "@tanstack/react-router";
import UserSettingsPage from "@/UserSettingsPage.tsx";

export const Route = createLazyFileRoute("/usersettings")({
    component: UserSettingsPage,
});