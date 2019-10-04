$(() => {
    const alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".split("");

    for (const letter of alphabet) {
        const button = $("<button>");
        button.text(letter);
        button.appendTo("#buttons");
    }

});