import { CSLJSON } from "@/utils/CSLJSON";
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button.tsx";
import { postSingleReference } from "@/api/postSingleReference.ts";
import { useNavigate } from "@tanstack/react-router";

export function SearchNode({ data }: { data: CSLJSON }) {
    const navigate = useNavigate({ from: "/search" });
    async function handlePostReference() {
        await postSingleReference(data).then(() =>
            navigate({ to: "/library" }),
        );
    }
    // TODO Add success/error alerts
    return (
        <div className="mt-2">
            <Card>
                <CardHeader>
                    <CardTitle>{data.title}</CardTitle>
                    <CardDescription>{data.keyword}</CardDescription>
                </CardHeader>
                <CardContent>
                    <p>{data.title}</p>
                    <p>{data.type}</p>
                    <p>{data.ISBN}</p>
                    {data.author?.map((a) => {
                        return <p>{a.family}</p>;
                    })}
                </CardContent>
                <CardFooter>
                    <Button onClick={handlePostReference}>Add?</Button>
                </CardFooter>
            </Card>
        </div>
    );
}
