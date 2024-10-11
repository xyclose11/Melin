import { Button } from "@/components/ui/button.tsx";
import { Link } from "react-router-dom";

export function WorkspaceToolBar() {
    return (
        <nav className="flex-1 top-16 fixed">
            <ul className="space-y-2">
                <li>
                    <Button className="w-full text-left">
                        <Link to="/CreateReferencePage" />
                    </Button>
                </li>
                <li>
                    <Button className="w-full text-left">
                        Your References
                    </Button>
                </li>
                <li>
                    <Button className="w-full text-left">Your Groups</Button>
                </li>
                <li>
                    <Button className="w-full text-left">Your Tags</Button>
                </li>
                <li>
                    <Button className="w-full text-left">Settings</Button>
                </li>
            </ul>
        </nav>
    );
}
