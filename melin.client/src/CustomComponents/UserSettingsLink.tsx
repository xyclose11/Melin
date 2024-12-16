import { Link } from "@tanstack/react-router";

export default function UserSettingsLink() {
    return (
        <Link
            to="/usersettings"
            className="text-muted-foreground transition-colors hover:text-foreground"
        >
            User Settings
        </Link>
    );
}
