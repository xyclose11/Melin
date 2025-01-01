import { Card, CardContent, CardFooter } from "@/components/ui/card.tsx";
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

export function RawView({ name }: { name: string }) {
    const { control, register } = useFormContext();

    return (
        <div className="mt-2">
            <Card className="w-[100%] pt-5">
                <CardContent>
                    <FormField
                        control={control}
                        name={`${name}`}
                        render={() => (
                            <FormItem>
                                <FormControl>
                                    <Textarea
                                        placeholder=""
                                        className="min-h-[250px] resize-none p-2.5 w-full text-sm text-gray-900 rounded-lg border border-gray-300"
                                        defaultValue={""}
                                        {...register(`${name}` as const)}
                                    />
                                </FormControl>
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
