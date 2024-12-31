import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
} from "@/components/ui/card.tsx";
import {
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form";
import { Textarea } from "@/components/ui/textarea";
import { useFormContext } from "react-hook-form";
import { useEffect } from "react";

export function RawView({ name }: { name: string }) {
    const {
        control,
        setValue,
        getValues,
        register,
        formState: { errors },
    } = useFormContext();

    useEffect(() => {
        console.log(getValues());
    }, []);

    return (
        <div>
            <Card>
                <CardHeader>
                    <CardTitle>Raw View</CardTitle>
                    <CardDescription>Here is the Raw view!</CardDescription>
                </CardHeader>
                <CardContent>
                    <FormField
                        control={control}
                        name={`${name}`}
                        render={() => (
                            <FormItem>
                                <FormLabel>Data</FormLabel>
                                <FormControl>
                                    <Textarea
                                        placeholder=""
                                        className="resize-none"
                                        defaultValue={""}
                                        {...register(`${name}` as const)}
                                    />
                                </FormControl>
                                <FormDescription>
                                    You can manually edit the parsed data here
                                    before saving it.
                                </FormDescription>
                                <FormMessage />
                            </FormItem>
                        )}
                    />
                </CardContent>
                <CardFooter>{/*    Display error messages HERE*/}</CardFooter>
            </Card>
        </div>
    );
}
