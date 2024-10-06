import { Link } from "react-router-dom";

export default function UserSettingsLink() {
    return (
        <Link
            to={"/user-settings"}
            className="text-muted-foreground transition-colors hover:text-foreground"
        >
            User Settings
        </Link>
    );
}
