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
import { PlusCircle } from "lucide-react";

export function SearchNode({ data }: { data: CSLJSON }) {
    const navigate = useNavigate({ from: "/search" });
    async function handlePostReference() {
        await postSingleReference(data).then(() =>
            navigate({ to: "/library" }),
        );
    }

    return (
        <div className="mt-2 p-1">
            <Card>
                <CardHeader>
                    <CardTitle>Title: {data.title}</CardTitle>
                    <CardDescription>Keywords: {data.keyword}</CardDescription>
                </CardHeader>
                <CardContent className="grid grid-cols-2 gap-2">
                    <div>
                        <p className="font-bold">
                            <a href={data.URL}>Access URL</a>
                        </p>
                    </div>
                    <div className="flex gap-2">
                        <h3 className="font-bold">Type:</h3>
                        <p className="capitalize">{data.type}</p>
                    </div>
                    <div className="flex gap-2">
                        <h3 className="font-bold">ISBN:</h3>
                        <p>{data.ISBN}</p>
                    </div>
                    <div className="flex gap-2">
                        <h3 className="font-bold">Number of Pages:</h3>
                        <p>{data["number-of-pages"]}</p>
                    </div>
                    <div className="flex gap-2">
                        <h3 className="font-bold">Language:</h3>
                        <p className="capitalize">{data.language}</p>
                    </div>
                    <div className="flex gap-2">
                        <h3 className="font-bold">Publisher:</h3>
                        <p className="capitalize">{data.publisher}</p>
                    </div>

                    {data.issued !== undefined && (
                        <div className="flex gap-2">
                            <h3 className="font-bold">Date Published:</h3>
                            <p>
                                {data?.issued?.["date-parts"]?.[0][0] +
                                    "/" +
                                    data.issued?.["date-parts"]?.[0][1] +
                                    "/" +
                                    data.issued?.["date-parts"]?.[0][2]}
                            </p>
                        </div>
                    )}
                    <div className="flex gap-2">
                        <h3 className="font-bold">Author(s):</h3>
                        {data.author?.map((a) => {
                            return (
                                <p key={a.given}>
                                    {a.suffix} {a.given}, {a.family}
                                </p>
                            );
                        })}
                    </div>
                </CardContent>
                <CardFooter className="justify-end m-0">
                    <Button
                        variant="outline"
                        size="icon"
                        onClick={handlePostReference}
                    >
                        <PlusCircle />
                    </Button>
                </CardFooter>
            </Card>
        </div>
    );
}
