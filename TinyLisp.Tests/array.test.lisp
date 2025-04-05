(defvar global-list '(1 2 3))

(deftest index-the-list ()
  (assert-eq (get global-list 1) 2))

(deftest array-map ()
  (defun plus-one (a) (+ 1 a))
  (let ((the-new-list (map global-list plus-one)))
    (assert-eq (get the-new-list 0) 2)
    (assert-eq (get the-new-list 1) 3)
    (assert-eq (get the-new-list 2) 4)))

(deftest array-map-with-closure ()
  (let ((the-list (map global-list (fn (a) (+ a 1)))))
    (assert-eq (get the-list 0) 2)))

(deftest array-count ()
  (assert-eq (count global-list) 3))
