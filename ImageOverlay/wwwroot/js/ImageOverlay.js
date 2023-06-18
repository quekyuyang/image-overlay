export default class ImageOverlay {
    constructor(img, canvas) {
        this.img = img;
        this.canvas = canvas;
        this.annotationDrawer = new AnnotationDrawer(canvas);

        this.viewPos = [0, 0];
        this.scale = 1;
        const self = this;
        this.canvas.addEventListener("mousedown", () => { self.mousedownHandler() });
        this.canvas.addEventListener("mouseup", () => { self.mouseupHandler() });

        this.draw();
    }

    mousedownHandler() {
        this.canvas.addEventListener("mousemove", this.mousemoveHandler);
    }

    mouseupHandler() {
        this.canvas.removeEventListener("mousemove", this.mousemoveHandler);
    }

    draw() {
        if (this.canvas.getContext) {
            const ctx = this.canvas.getContext("2d");
            ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);

            const scaledWidth = this.img.naturalWidth * this.scale;
            const scaledHeight = this.img.naturalHeight * this.scale;
            ctx.drawImage(this.img, this.viewPos[0], this.viewPos[1], scaledWidth, scaledHeight);
        }

        this.annotationDrawer.draw(this.viewPos);
    }

    // annotationSet: {name: annotation set name, annotations: annotation name to coordinate array mappings}
    addAnnotationSet(annotationSet, checkboxes) {
        this.annotationDrawer.addAnnotationSet(annotationSet, createCheckboxDict(checkboxes));
    }

    mousemoveHandler = (mouseMoveEvent) => {
        this.viewPos[0] += mouseMoveEvent.movementX;
        this.viewPos[1] += mouseMoveEvent.movementY;
        this.draw();
    }
}


function createCheckboxDict(checkboxes) {
    const dict = {};
    for (const checkbox of checkboxes) {
        dict[checkbox.id] = checkbox;
    }
    return dict;
}


class AnnotationDrawer {
    colors = ["#6f8b74",
        "#69dcc4",
        "#b66ebd",
        "#dbd376",
        "#76e64d",
        "#598fc5",
        "#d45c72",
        "#a854e7",
        "#a77986",
        "#cd7d5c",
        "#78c2d0",
        "#b2d950",
        "#d94bcb",
        "#c29b34",
        "#e0d831",
        "#dbc7b0",
        "#9e8452",
        "#b1badc",
        "#bad9a0",
        "#d8812c",
        "#6e84d4",
        "#e04490",
        "#df4a35",
        "#736fe5",
        "#4b9963",
        "#5f8197",
        "#7e9137",
        "#dda2d0",
        "#6de190",
        "#4ea838"
    ];

    constructor(canvas) {
        this.annotationSets = {};
        this.canvas = canvas;
        this.itColors = this.colors[Symbol.iterator]();
    }

    draw(viewPos) {
        if (this.canvas.getContext) {
            const ctx = this.canvas.getContext("2d");

            for (const setName in this.annotationSets) {
                const annotationSet = this.annotationSets[setName];
                for (const annotationName in annotationSet) {
                    const annotation = annotationSet[annotationName];
                    ctx.fillStyle = annotation.color;
                    if (annotation.checkbox.checked) {
                        for (const pos of annotation.coords) {
                            ctx.fillRect(viewPos[0] + pos[0], viewPos[1] + pos[1], 1, 1);
                        }
                    }
                }
            }
        }
    }

    // annotationSet: {name: annotation set name, annotations: annotation name to coordinate array mappings}
    addAnnotationSet(annotationSet, checkboxes) {
        const setName = annotationSet.name;
        const annotations = {};
        for (const annotationName in annotationSet.annotations) {
            const color = this.itColors.next().value;
            if (color) {
                annotations[annotationName] = new Annotation(annotationSet.annotations[annotationName], color, checkboxes[annotationName]);
            }
        }
        this.annotationSets[setName] = annotations;
    }
}

class Annotation {
    constructor(coords, color, checkbox) {
        this.coords = coords;
        this.color = color;
        this.checkbox = checkbox;
    }
}
