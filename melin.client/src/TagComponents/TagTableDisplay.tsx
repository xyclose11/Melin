import { Badge } from "../components/ui/badge.tsx";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuSub,
    DropdownMenuSubContent,
    DropdownMenuSubTrigger,
    DropdownMenuTrigger,
} from "../components/ui/dropdown-menu.tsx";
import { instance } from "../utils/axiosInstance.ts";
import { EditTagForm } from "../TagComponents/EditTagForm.tsx";

export function TagTableDisplay({
    name,
    tagId,
    refId,
    handleRemoveTag,
}: {
    name: string;
    tagId: number;
    refId: number;
    handleRemoveTag: (tagToRemoveId: any) => void;
}) {
    if (name === undefined) {
        name = "";
    }

    const removeTagFromRef = async () => {
        try {
            const res = await instance.post(
                `remove-tag-on-reference?tagId=${tagId}&refId=${refId}`,
                null,
                {
                    withCredentials: true,
                },
            );

            if (res.status === 200) {
                handleRemoveTag(tagId);
            }
        } catch (e) {
            console.error(e);
        }
    };

    return (
        <>
            <DropdownMenu>
                <DropdownMenuTrigger>
                    <Badge variant="secondary">
                        {name.length > 16 ? name.slice(0, 16) + "..." : name}
                    </Badge>
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                    <DropdownMenuSub>
                        <DropdownMenuSubTrigger>Edit</DropdownMenuSubTrigger>
                        <DropdownMenuSubContent>
                            <EditTagForm tagText={name} />
                        </DropdownMenuSubContent>
                    </DropdownMenuSub>
                    <DropdownMenuItem onClick={removeTagFromRef}>
                        Remove From Reference
                    </DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </>
    );
}
