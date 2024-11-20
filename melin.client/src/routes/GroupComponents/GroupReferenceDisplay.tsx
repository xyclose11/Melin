import { File } from "lucide-react";
import { GroupReferenceSchema } from "@/routes/GroupComponents/DraggableGroup.tsx";
import {
    ContextMenu,
    ContextMenuContent,
    ContextMenuItem,
    ContextMenuTrigger,
} from "@/components/ui/context-menu.tsx";
import { Link } from "react-router-dom";
import { toast } from "@/hooks/use-toast.ts";
import { instance } from "@/utils/axiosInstance.ts";

export function GroupReferenceDisplay({
    gn,
    groupName,
}: {
    gn: GroupReferenceSchema;
    groupName: string;
}) {
    const removeReferenceFromGroup = async () => {
        try {
            const res = await instance.put(
                `remove-refs-from-group?groupName=${groupName}&referenceId=${gn.id}`,
                null,
                { withCredentials: true },
            );

            if (res.status === 200) {
                toast({
                    variant: "default",
                    title: "Successfully removed reference from group",
                });
            } else {
                toast({
                    variant: "destructive",
                    title: "Unable to removed reference from group",
                });
            }
        } catch (e) {
            toast({
                variant: "destructive",
                title: `Unable to remove reference: ${gn.title} from group`,
            });
        }
    };

    return (
        <div className={"flex gap-1 m-1"}>
            <File size={16} strokeWidth={0.5} />
            <div className={"hover:bg-accent w-full"}>
                <ContextMenu>
                    <ContextMenuTrigger>
                        <Link
                            className="capitalize"
                            to={`/edit-reference/${gn.id}`}
                        >
                            {gn.title}
                        </Link>
                    </ContextMenuTrigger>
                    <ContextMenuContent>
                        <ContextMenuItem onClick={removeReferenceFromGroup}>
                            Remove
                        </ContextMenuItem>
                    </ContextMenuContent>
                </ContextMenu>
            </div>
        </div>
    );
}
