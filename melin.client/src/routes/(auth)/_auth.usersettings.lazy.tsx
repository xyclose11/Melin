import { createLazyFileRoute } from "@tanstack/react-router";
import { UserSettings } from "@/UserSettingsPage.tsx";

export const Route = createLazyFileRoute("/(auth)/_auth/usersettings")({
    component: UserSettings,
});
