import ImageOverlay from "./ImageOverlay.js";


let imageOverlay;
document.body.onload = () => {
    const img = document.getElementById("image");
    imageOverlay = new ImageOverlay(img, document.getElementById("canvas"));

    const annotationDownloadManager = new AnnotationDownloadManager();

    const selectElem = document.getElementById("annotation-set-select");

    selectElem.addEventListener("change", annotationDownloadManager.downloadHandler);
}


function draw() {
    imageOverlay.draw();
}


class AnnotationDownloadManager {
    constructor() {
        this.downloaded = [];
    }

    get downloadHandler() {
        return (event) => {            
            if (!this.downloaded.includes(event.target.value)) {
                fetch(event.target.value)
                    .then(res => res.text())
                    .then(text => JSON.parse(text))
                    .then(json => {
                        const checkboxes = initCheckboxes(json, event.target.selectedOptions[0].innerText);
                        const annotationSet = {
                            name: event.target.selectedOptions[0].innerText,
                            annotations: json
                        };
                        imageOverlay.addAnnotationSet(annotationSet, checkboxes);
                        imageOverlay.draw();
                    })
                this.downloaded.push(event.target.value);
            }
        }
    }
}


function initCheckboxes(json, setName) {
    const checkboxes = [];
    const annotationToggle = document.getElementById("annotation-toggle");
    const checkboxSet = document.createElement("div");
    const checkboxSetName = document.createElement("div");
    checkboxSetName.innerText = setName;
    checkboxSet.appendChild(checkboxSetName)
    annotationToggle.appendChild(checkboxSet);
    for (const annotationName in json) {
        const li = document.createElement("li");
        checkboxSet.appendChild(li);

        const checkbox = document.createElement("input");
        checkbox.id = annotationName;
        checkbox.className = "toggle";
        checkbox.type = "checkbox";
        checkbox.value = annotationName;
        checkbox.onchange = draw;
        li.appendChild(checkbox);

        const label = document.createElement("label");
        label.htmlFor = annotationName;
        label.innerText = annotationName;
        li.appendChild(label);

        checkboxes.push(checkbox);
    }
    return checkboxes;
}
