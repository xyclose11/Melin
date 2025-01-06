export const debounce = (mainFunction: Function, delay: number) => {
    let timer: ReturnType<typeof setTimeout>;

    return function (...args: any[]) {
        clearTimeout(timer);

        timer = setTimeout(() => {
            mainFunction(...args);
        }, delay);
    };
};
