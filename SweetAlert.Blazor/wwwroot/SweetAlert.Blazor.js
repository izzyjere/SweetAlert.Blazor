
// functions, and may import other JavaScript modules if required.

export function loadSweetAlert() {
    const existingScript = document.getElementById('sweetAlert');

    if (!existingScript) {
        const script = document.createElement('script');
        script.src = '_content/SweetAlert.Blazor/lib/sweetalert.min.js';
        script.id = 'sweetAlert';
        document.body.appendChild(script);

    }
}

export async function showAlert(title, message, severity) {
    return await swal(title,message,severity);
}
export async function showConfirm(title, message,severity, confirmText, cancelText, confirmClass , cancelClass , dangerMode) {
    let confirm = false
    let result = await swal({
        title: title,
        text: message,
        icon: severity,
        buttons: {
            cancel: {
                text: cancelText,
                value: null,
                visible: true,
                className: cancelClass,
                closeModal: true,
            },
            confirm: {
                text: confirmText,
                value: true,
                visible: true,
                className: confirmClass,
                closeModal: true
            }
        },
        dangerMode: dangerMode
    })
    if (result) {
        confirm = true
    }
    return confirm
}
export function showAlertComplex(swalOptions) {
    return Swal.fire(swalOptions);
}
