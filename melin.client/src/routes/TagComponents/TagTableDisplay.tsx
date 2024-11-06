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
import { instance } from "@/utils/axiosInstance.ts";

export function TagTableDisplay({
    name,
    tagId,
    refId,
}: {
    name: string;
    tagId: number;
    refId: number;
}) {
    const removeTagFromRef = async () => {
        try {
            const res = await instance.post(
                `remove-tag-on-reference?tagId=${tagId}&refId=${refId}`,
                null,
                {
                    withCredentials: true,
                },
            );
            console.log(res);
        } catch (e) {
            console.error(e);
        }
    };

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
                    <DropdownMenuItem onClick={removeTagFromRef}>
                        Remove From Reference
                    </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </>
    );
}
