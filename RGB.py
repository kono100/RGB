import cv2
import numpy as np

# Função de callback para ajustar os parâmetros HSV interativamente
def atualizar_parametros_trackbar(value):
    pass

# Função para destacar uma cor específica (R, G, B) na imagem
def destacar_cor_rgb(imagem, r_range, g_range, b_range):
    # Definir intervalos de valores RGB dinamicamente
    lower_rgb = np.array([b_range[0], g_range[0], r_range[0]])
    upper_rgb = np.array([b_range[1], g_range[1], r_range[1]])

    # Criar máscara para filtrar a cor específica
    mask = cv2.inRange(imagem, lower_rgb, upper_rgb)

    # Aplicar a máscara à imagem original
    imagem_destacada = cv2.bitwise_and(imagem, imagem, mask=mask)

    return imagem_destacada

# Inicializar a janela de controle
cv2.namedWindow('Controle de Cores')

# Adicionar barras de controle para ajustar os parâmetros RGB
cv2.createTrackbar('Red Min', 'Controle de Cores', 0, 255, atualizar_parametros_trackbar)
cv2.createTrackbar('Red Max', 'Controle de Cores', 255, 255, atualizar_parametros_trackbar)
cv2.createTrackbar('Green Min', 'Controle de Cores', 0, 255, atualizar_parametros_trackbar)
cv2.createTrackbar('Green Max', 'Controle de Cores', 255, 255, atualizar_parametros_trackbar)
cv2.createTrackbar('Blue Min', 'Controle de Cores', 0, 255, atualizar_parametros_trackbar)
cv2.createTrackbar('Blue Max', 'Controle de Cores', 255, 255, atualizar_parametros_trackbar)

# Carregar a imagem da maçã
imagem_maca = cv2.imread('maca.png')

while True:
    # Obter valores dos controles deslizantes
    red_min = cv2.getTrackbarPos('Red Min', 'Controle de Cores')
    red_max = cv2.getTrackbarPos('Red Max', 'Controle de Cores')
    green_min = cv2.getTrackbarPos('Green Min', 'Controle de Cores')
    green_max = cv2.getTrackbarPos('Green Max', 'Controle de Cores')
    blue_min = cv2.getTrackbarPos('Blue Min', 'Controle de Cores')
    blue_max = cv2.getTrackbarPos('Blue Max', 'Controle de Cores')

    # Destacar a cor específica (R, G, B) na imagem
    imagem_destacada = destacar_cor_rgb(imagem_maca, (red_min, red_max), (green_min, green_max), (blue_min, blue_max))

    # Mostrar a imagem original e a imagem com a cor específica destacada
    cv2.imshow('Imagem Original', imagem_maca)
    cv2.imshow('Cores Destacadas', imagem_destacada)

    # Aguardar por uma tecla e verificar se é a tecla 'q' para sair do loop
    key = cv2.waitKey(1) & 0xFF
    if key == ord('q'):
        break

cv2.destroyAllWindows()
