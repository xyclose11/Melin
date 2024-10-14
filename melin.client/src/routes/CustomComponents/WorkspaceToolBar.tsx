import { Link } from "react-router-dom";
import { SquarePlus } from "lucide-react";
import { Label } from "@/components/ui/label.tsx";

export function WorkspaceToolBar() {
    return (
        <nav className="flex-1">
            <ul className="space-y-2">
                <li>
                    <Link
                        to={"/create-reference"}
                        className="flex items-center gap-2 text-foreground font-semibold"
                    >
                        <SquarePlus />
                        <Label className={"w-full text-left"}> Create </Label>
                    </Link>
                </li>
                <li>
                    <Link
                        to={"/create-reference"}
                        className="flex items-center gap-2 text-foreground font-semibold"
                    >
                        <SquarePlus />
                        <Label className={"w-full text-left"}>
                            {" "}
                            View References{" "}
                        </Label>
                    </Link>
                </li>
                <li>
                    <Link
                        to={"/create-reference"}
                        className="flex items-center gap-2 text-foreground font-semibold"
                    >
                        <SquarePlus />
                        <Label className={"w-full text-left"}> Groups </Label>
                    </Link>
                </li>
                <li>
                    <Link
                        to={"/create-reference"}
                        className="flex items-center gap-2 text-foreground font-semibold"
                    >
                        <SquarePlus />
                        <Label className={"w-full text-left"}> Tags </Label>
                    </Link>
                </li>
                <li>
                    <Link
                        to={"/create-reference"}
                        className="flex items-center gap-2 text-foreground font-semibold"
                    >
                        <SquarePlus />
                        <Label className={"w-full text-left"}> Settings </Label>
                    </Link>
                </li>
            </ul>
        </nav>
    );
}
