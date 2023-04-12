// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.
export function loadSweetAlert(callback) {
    const existingScript = document.getElementById('sweetAlert');

    if (!existingScript) {
        const script = document.createElement('script');
        script.src = '_content/SweetAlert.Blazor/lib/sweetalert.min.js';
        script.id = 'sweetAlert';
        document.body.appendChild(script);

        script.onload = () => {
            if (callback) callback();
        };
    }

    if (existingScript && callback) callback();
}

export function showPrompt(message) {
  return prompt(message, 'Type anything here');
}
export function showAlert(title, message, type) {

}
export function showAlertComplex(title, renderFragment) {

}
