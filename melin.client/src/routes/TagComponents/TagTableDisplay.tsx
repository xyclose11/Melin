import { Badge } from "@/components/ui/badge.tsx";
import { CreateGroupForm } from "@/routes/GroupComponents/CreateGroupForm.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSub,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu.tsx";

export function TagTableDisplay({
    name,
    tagId,
}: {
    name: string;
    tagId: number;
}) {
    return (
        <>
            <DropdownMenu>
                <DropdownMenuTrigger>
                    <Badge variant="secondary">{name}</Badge>
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                    <DropdownMenuItem>
                        <DropdownMenuSub>
                            <DropdownMenuSubTrigger>
                                Edit
                            </DropdownMenuSubTrigger>
                            <DropdownMenuSubContent>
                                <CreateGroupForm />
                            </DropdownMenuSubContent>
                        </DropdownMenuSub>
                    </DropdownMenuItem>
                    <DropdownMenuItem>Remove From Reference</DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </>
    );
}
